function AbrirMenu() {
    var background = document.getElementsByClassName('background-menu-responsive')[0];
    background.style.display = 'block';

    var menu = document.getElementsByClassName('mid-menu-responsive')[0];
    menu.style.marginLeft = "0%";

    var lista = document.getElementsByTagName('li');

    for (var i = 0; i < lista.length; i++) {
        lista[i].style.marginLeft = '20%';
        lista[i].style.opacity = '1';
    }
}

function FecharMenu() {
    var background = document.getElementsByClassName('background-menu-responsive')[0];
    background.style.display = 'none';

    var menu = document.getElementsByClassName('mid-menu-responsive')[0];
    menu.style.marginLeft = "-70vw";

    var lista = document.getElementsByTagName('li');

    for (var i = 0; i < lista.length; i++) {
        lista[i].style.marginLeft = '-20%';
        lista[i].style.opacity = '0';
    }
}

function SelecionarArquivo(idBloco) {
    var bloco = document.getElementsByClassName('box-television')[idBloco];
    bloco.style.backgroundColor = 'mediumseagreen';
}

function SelecionarArquivoEdit() {
    var bloco = document.getElementsByClassName('button-add-television')[0];
    bloco.style.backgroundColor = 'mediumseagreen';
}

function FecharModalAviso() {
    var modal = document.getElementsByClassName('warning')[0];
    modal.style.display = 'none';
}