function ShowLoad() {
    var load = document.getElementsByClassName('loading-page')[0];
    load.style.display = 'block';
}

function fecharModalError() {
    var mensagem = document.getElementById('text-error');
    mensagem.style.opacity = '0';

    var modal = document.getElementsByClassName('erro-message')[0];
    modal.style.marginLeft = '-350px';
}

function abrirModalError() {
    var mensagem = document.getElementById('text-error');
    mensagem.style.opacity = '1';

    var modal = document.getElementsByClassName('erro-message')[0];
    modal.style.marginLeft = '-20px';
}