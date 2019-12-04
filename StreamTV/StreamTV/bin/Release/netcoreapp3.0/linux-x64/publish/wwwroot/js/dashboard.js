function ShowLoad() {
    var load = document.getElementsByClassName('loading-page')[0];
    load.style.display = 'block';
}

function HideLoad() {
    var load = document.getElementsByClassName('loading-page')[0];
    load.style.display = 'none';
}

function AbrirMenu() {
    var background = document.getElementsByClassName('background-menu-responsive')[0];
    background.style.display = 'block';

    var menu = document.getElementsByClassName('mid-menu-responsive')[0];
    menu.style.marginLeft = "0vw";

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
    menu.style.marginLeft = "-70vw;";

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

function AlterarMetodoForm() {
    var form = document.getElementById("form-edit-televison");
    form.method = "get";
    form.submit();
}

function DeletarTelevisaoAJAX(_codigoTelevision) {
    ShowLoad();

    var urlRequest = '/Home/DeleteTelevision';

    var objetoGeneroDeletar = {
        codigoTelevision: _codigoTelevision
    }

    var divWarning = document.createElement('div');
    var contentDiv = ""; 
                

    $.ajax({
        url: urlRequest,
        data: objetoGeneroDeletar,
        type: 'DELETE',
        success: function (result) {
            divWarning.className = 'warning';
            contentDiv = `<div class="modal-warning">
                    <center>
                        <h3>Aviso</h3>
                        <br />
                        <p>${result}</p>
                        <br />
                        <button type="button" class="btn btn-primary" onclick="window.location.href='/Home/Index'">Início</button>
                    </center>
                </div>`
            divWarning.innerHTML = contentDiv;
            HideLoad();
        },
        error: function (xhr, ajaxOptions, thrownError) {
            divWarning.className = 'warning';
            contentDiv = `<div class="modal-warning">
                    <center>
                        <h3>Aviso</h3>
                        <br />
                        <p>${thrownError}</p>
                        <br />
                        <button type="button" class="btn btn-primary" onclick="window.location.href='/Home/Index'">Início</button>
                    </center>
                </div>`
            divWarning.innerHTML = contentDiv;
            HideLoad();
        }
    });

    document.getElementsByTagName('body')[0].appendChild(divWarning);
}


function DeletarVideoAJAX(_codigoTelevision, _idVideo) {
    ShowLoad();

    var urlRequest = '/Home/DeleteVideo';

    var objetoGeneroDeletar = {
        codigoTelevision: _codigoTelevision,
        idVideo: _idVideo
    }

    var divWarning = document.createElement('div');
    var contentDiv = "";


    $.ajax({
        url: urlRequest,
        data: objetoGeneroDeletar,
        type: 'DELETE',
        success: function (result) {
            divWarning.className = 'warning';
            contentDiv = `<div class="modal-warning">
                    <center>
                        <h3>Aviso</h3>
                        <br />
                        <p>${result}</p>
                        <br />
                        <button type="button" class="btn btn-primary" onclick="window.location.href='/Home/Index'">Início</button>
                    </center>
                </div>`
            divWarning.innerHTML = contentDiv;
            HideLoad();
        },
        error: function (xhr, ajaxOptions, thrownError) {
            divWarning.className = 'warning';
            contentDiv = `<div class="modal-warning">
                    <center>
                        <h3>Aviso</h3>
                        <br />
                        <p>${thrownError}</p>
                        <br />
                        <button type="button" class="btn btn-primary" onclick="window.location.href='/Home/Index'">Início</button>
                    </center>
                </div>`
            divWarning.innerHTML = contentDiv;
            HideLoad();
        }
    });

    document.getElementsByTagName('body')[0].appendChild(divWarning);
}