import React, { useCallback, useEffect, useState } from 'react';
import { ActivityIndicator, Alert, FlatList, SafeAreaView, ScrollView, StatusBar, StyleSheet, Text, TextInput, TouchableOpacity, View } from 'react-native';

// Android Emulator: 10.0.2.2. Em aparelho físico, use o IPv4 do computador na rede Wi-Fi.
const API_BASE_URL = 'http://10.0.2.2:8080';
type Produto = { id:number; nome:string; categoria:string; preco:number; estoque:number; ativo:boolean };
type Cliente = { id:number; nome:string; telefone:string; email:string; ativo:boolean };
type Pedido = { id:number; status:string; valorTotal:number; criadoEm:string };

export default function App() {
  const [tab,setTab] = useState<'catalogo'|'pedidos'>('catalogo');
  const [produtos,setProdutos] = useState<Produto[]>([]);
  const [clientes,setClientes] = useState<Cliente[]>([]);
  const [pedidos,setPedidos] = useState<Pedido[]>([]);
  const [clienteId,setClienteId] = useState('');
  const [quantidades,setQuantidades] = useState<Record<number,string>>({});
  const [loading,setLoading] = useState(true);
  const [sending,setSending] = useState(false);
  const [error,setError] = useState('');

  const load = useCallback(async () => {
    setLoading(true); setError('');
    try {
      const [p,c,o] = await Promise.all([
        fetch(`${API_BASE_URL}/api/produtos`), fetch(`${API_BASE_URL}/api/clientes`), fetch(`${API_BASE_URL}/api/pedidos`)
      ]);
      if (!p.ok || !c.ok || !o.ok) throw new Error('A API retornou uma resposta inválida.');
      const [pd,cd,od] = await Promise.all([p.json(),c.json(),o.json()]);
      setProdutos(pd.filter((x:Produto)=>x.ativo)); setClientes(cd.filter((x:Cliente)=>x.ativo)); setPedidos(od);
      if (!clienteId && cd.length) setClienteId(String(cd[0].id));
    } catch { setError('Não foi possível conectar à API. Confira se o servidor está ativo e se o endereço configurado está correto.'); }
    finally { setLoading(false); }
  }, [clienteId]);

  useEffect(()=>{load();},[load]);
  const placeOrder = async (product:Produto) => {
    const qty = Number(quantidades[product.id] || '1');
    if (!clienteId || !Number.isInteger(qty) || qty < 1) { Alert.alert('Confira os dados','Selecione um cliente e informe uma quantidade válida.'); return; }
    setSending(true);
    try {
      const response = await fetch(`${API_BASE_URL}/api/pedidos`, {method:'POST',headers:{'Content-Type':'application/json'},body:JSON.stringify({clienteId:Number(clienteId),itens:[{produtoId:product.id,quantidade:qty}]})});
      const body = await response.json().catch(()=>null);
      if (!response.ok) throw new Error(body?.message || 'Não foi possível registrar o pedido.');
      Alert.alert('Pedido registrado',`Pedido #${body.id} criado com sucesso.`);
      setQuantidades({...quantidades,[product.id]:'1'}); await load(); setTab('pedidos');
    } catch(e:any) { Alert.alert('Atenção',e.message || 'Falha ao enviar pedido.'); }
    finally { setSending(false); }
  };

  return <SafeAreaView style={s.screen}><StatusBar barStyle="light-content" backgroundColor="#241b17"/>
    <View style={s.header}><Text style={s.brand}>slowcoffee</Text><Text style={s.subtitle}>CAFÉ COM EXPERIÊNCIA · PEDIDOS MOBILE</Text><View style={s.headerLine}/></View>
    <View style={s.tabs}><TouchableOpacity onPress={()=>setTab('catalogo')} style={[s.tab,tab==='catalogo'&&s.tabActive]}><Text style={[s.tabText,tab==='catalogo'&&s.tabTextActive]}>Cardápio</Text></TouchableOpacity><TouchableOpacity onPress={()=>setTab('pedidos')} style={[s.tab,tab==='pedidos'&&s.tabActive]}><Text style={[s.tabText,tab==='pedidos'&&s.tabTextActive]}>Meus pedidos</Text></TouchableOpacity></View>
    {loading?<View style={s.center}><ActivityIndicator color="#946b4e" size="large"/><Text style={s.muted}>Conectando ao Slow Coffee...</Text></View>:error?<View style={s.center}><Text style={s.error}>{error}</Text><TouchableOpacity style={s.button} onPress={load}><Text style={s.buttonText}>Tentar novamente</Text></TouchableOpacity></View>:tab==='catalogo'?<ScrollView contentContainerStyle={s.content}>
      <Text style={s.heading}>Feito para sua pausa.</Text><Text style={s.description}>Escolha seus favoritos e envie o pedido à cafeteria.</Text>
      <Text style={s.label}>IDENTIFIQUE O CLIENTE</Text><ScrollView horizontal showsHorizontalScrollIndicator={false} style={{marginBottom:18}}>{clientes.map(c=><TouchableOpacity key={c.id} onPress={()=>setClienteId(String(c.id))} style={[s.customer,clienteId===String(c.id)&&s.customerSelected]}><Text style={[s.customerText,clienteId===String(c.id)&&s.customerTextSelected]}>{c.nome}</Text></TouchableOpacity>)}</ScrollView>
      {produtos.map(p=><View key={p.id} style={s.product}><View style={s.productTop}><View style={s.coffeeIcon}><Text>☕</Text></View><View style={{flex:1}}><Text style={s.productName}>{p.nome}</Text><Text style={s.category}>{p.categoria} · {p.estoque} disponíveis</Text></View><Text style={s.price}>R$ {p.preco.toFixed(2).replace('.',',')}</Text></View><View style={s.orderRow}><TextInput value={quantidades[p.id]||'1'} onChangeText={v=>setQuantidades({...quantidades,[p.id]:v.replace(/[^0-9]/g,'')})} keyboardType="number-pad" style={s.qty} accessibilityLabel={`Quantidade de ${p.nome}`}/><TouchableOpacity disabled={sending || p.estoque===0} style={[s.button,s.orderButton]} onPress={()=>placeOrder(p)}><Text style={s.buttonText}>{sending?'Enviando...':'Adicionar pedido'}</Text></TouchableOpacity></View></View>)}
    </ScrollView>:<FlatList data={pedidos} keyExtractor={p=>String(p.id)} contentContainerStyle={s.content} ListHeaderComponent={<><Text style={s.heading}>Acompanhe seus pedidos.</Text><Text style={s.description}>Atualize a lista para consultar o andamento.</Text><TouchableOpacity onPress={load} style={s.refresh}><Text style={s.refreshText}>↻ Atualizar pedidos</Text></TouchableOpacity></>} ListEmptyComponent={<Text style={s.muted}>Nenhum pedido registrado.</Text>} renderItem={({item})=><View style={s.product}><View style={s.productTop}><View style={s.coffeeIcon}><Text>▤</Text></View><View style={{flex:1}}><Text style={s.productName}>Pedido #{item.id}</Text><Text style={s.category}>{new Date(item.criadoEm).toLocaleString('pt-BR')}</Text></View><Text style={s.price}>R$ {item.valorTotal.toFixed(2).replace('.',',')}</Text></View><Text style={s.orderStatus}>{item.status}</Text></View>}/>}
    <View style={s.footer}><Text style={s.footerText}>SLOW COFFEE  ·  UMA PAUSA FAZ BEM</Text></View>
  </SafeAreaView>;
}
const s=StyleSheet.create({screen:{flex:1,backgroundColor:'#f7f3ef'},header:{backgroundColor:'#241b17',paddingHorizontal:22,paddingTop:20,paddingBottom:17},brand:{fontSize:30,fontWeight:'800',letterSpacing:-1.5,color:'#fff'},subtitle:{fontSize:9,letterSpacing:2,color:'#c8aa93',marginTop:4},headerLine:{height:2,width:42,backgroundColor:'#b58a68',marginTop:15},tabs:{flexDirection:'row',backgroundColor:'#fff',paddingHorizontal:18,borderBottomWidth:1,borderBottomColor:'#eee6df'},tab:{paddingVertical:14,paddingHorizontal:16,marginRight:12,borderBottomWidth:2,borderBottomColor:'transparent'},tabActive:{borderBottomColor:'#79553e'},tabText:{fontSize:12,color:'#988b82',fontWeight:'600'},tabTextActive:{color:'#4a3022'},content:{padding:18,paddingBottom:30},heading:{fontSize:25,fontWeight:'800',color:'#30231c',letterSpacing:-.7},description:{fontSize:12,color:'#8c7e74',marginTop:6,marginBottom:20},label:{fontSize:9,letterSpacing:1.5,fontWeight:'800',color:'#8b796d',marginBottom:10},customer:{paddingVertical:9,paddingHorizontal:13,borderWidth:1,borderColor:'#e6ddd5',borderRadius:20,marginRight:8,backgroundColor:'#fff'},customerSelected:{backgroundColor:'#79553e',borderColor:'#79553e'},customerText:{fontSize:11,color:'#69584c'},customerTextSelected:{color:'#fff',fontWeight:'700'},product:{backgroundColor:'#fff',padding:15,borderRadius:15,marginBottom:12,borderWidth:1,borderColor:'#eee7e0'},productTop:{flexDirection:'row',alignItems:'center',gap:11},coffeeIcon:{width:40,height:40,borderRadius:13,backgroundColor:'#f3ece6',alignItems:'center',justifyContent:'center'},productName:{fontSize:14,fontWeight:'700',color:'#33251d'},category:{fontSize:10,color:'#9b8d82',marginTop:4},price:{fontSize:13,fontWeight:'800',color:'#79553e'},orderRow:{flexDirection:'row',justifyContent:'flex-end',gap:8,marginTop:13},qty:{width:54,borderWidth:1,borderColor:'#e5ddd5',borderRadius:9,textAlign:'center',padding:8,color:'#382920'},button:{backgroundColor:'#79553e',paddingHorizontal:16,paddingVertical:11,borderRadius:9,alignItems:'center'},orderButton:{minWidth:145},buttonText:{color:'#fff',fontWeight:'700',fontSize:11},center:{flex:1,alignItems:'center',justifyContent:'center',padding:28},muted:{fontSize:12,color:'#8c7e74',marginTop:12,textAlign:'center'},error:{fontSize:12,color:'#a34335',textAlign:'center',marginBottom:16},footer:{padding:13,alignItems:'center',borderTopWidth:1,borderTopColor:'#e9e0d8'},footerText:{fontSize:8,letterSpacing:1.5,color:'#a39489'},refresh:{alignSelf:'flex-start',marginBottom:15,paddingVertical:8},refreshText:{fontSize:11,color:'#79553e',fontWeight:'700'},orderStatus:{marginTop:10,alignSelf:'flex-start',backgroundColor:'#f3ece6',color:'#79553e',paddingVertical:5,paddingHorizontal:10,borderRadius:20,fontSize:10,fontWeight:'700'}});
