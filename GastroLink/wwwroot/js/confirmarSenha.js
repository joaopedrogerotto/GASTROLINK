let confirmarSenhaInput = document.getElementById('confirmSenha');
let senhaInput = document.getElementById('senha');

function validarSenha() {
    if (confirmarSenhaInput.value !== senhaInput.value) {
        confirmarSenhaInput.setCustomValidity('Senhas não coincidem.');
    } else {
        confirmarSenhaInput.setCustomValidity('');
    }
}

senhaInput.addEventListener('input', validarSenha);
confirmarSenhaInput.addEventListener('input', validarSenha);