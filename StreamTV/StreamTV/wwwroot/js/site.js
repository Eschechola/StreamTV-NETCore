function ShowLoad() {
    var load = document.getElementsByClassName('loading-page')[0];
    load.style.display = 'block';
}

function fecharModalError() {
    var mensagem = document.getElementById('text-error');
    mensagem.style.opacity = '0';

    var modal = document.getElementsByClassName('erro-message')[0];
    modal.style.marginLeft = '-100%';
}

function abrirModalError() {
    var mensagem = document.getElementById('text-error');
    mensagem.style.opacity = '1';

    var modal = document.getElementsByClassName('erro-message')[0];
    modal.style.marginLeft = '-2%';
}

function AbrirMenuSite() {
    var background = document.getElementsByClassName('background-menu-responsive')[0];
    background.style.display = 'block';

    var menu = document.getElementsByClassName('site-menu-responsive')[0];
    menu.style.marginLeft = "0%";

    var lista = document.getElementsByTagName('li');

    for (var i = 0; i < lista.length; i++) {
        lista[i].style.marginLeft = '20%';
        lista[i].style.opacity = '1';
    }
}

function FecharMenuSite() {
    var background = document.getElementsByClassName('background-menu-responsive')[0];
    background.style.display = 'none';

    var menu = document.getElementsByClassName('site-menu-responsive')[0];
    menu.style.marginLeft = "-70vw";

    var lista = document.getElementsByTagName('li');

    for (var i = 0; i < lista.length; i++) {
        lista[i].style.marginLeft = '-20%';
        lista[i].style.opacity = '0';
    }
}

