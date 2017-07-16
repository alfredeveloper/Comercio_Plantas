function irA(valor) {
    if (valor == 'store') {
        window.location = "/Store";
    } else if (valor == 'service') {
        window.location = "/Service";
    } else if (valor == 'blog') {
        window.location = "/Blog";
    }
    
};

function activaOpcion(valor) {
    var a = document.getElementById(valor);
    var b = a.classList.add('active');
}