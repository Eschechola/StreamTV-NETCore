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