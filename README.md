# StreamTV-NETCore
<h2>Aplicativo pra stream em televisões simultâneas</h2>
<br><br><br>
<p>Projeto feito a pedido da coordenação técnica de tecnologia da escola SENAI "Ary Torres", para uso nas televisões do local</p>
<br><br>
<strong>Desenvolvedores</strong>
<ul>
    <li>Lucas Eschechola</li>
    <li>Robson França</li>
</ul>
<br><br>
<h3>Informações de desenvolvimento</h3>
<br>
<strong>Rotas:</strong>
<br>
<p>Dados da televisao através do código</p><br>
<p>https://senaitvsapi.herokuapp.com/api/Television/GetByCode</p><br>
<p>Enviar o código da televisão via POST no BODY da requisição</p><br>
<p><strong>Retorno:</strong> Model televisoes</p>

<br><br>
<p>Lista de vídeos da televisão</p><br>
<p>https://senaitvsapi.herokuapp.com/api/Video/GetAllByCode</p><br>
<p>Enviar o código da televisão via POST no BODY da requisição</p><br>
<p><strong>Retorno:</strong> List do model Videos</p>
