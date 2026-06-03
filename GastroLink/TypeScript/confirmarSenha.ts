const confirmarSenhaInput = document.getElementById('confirmSenha') as HTMLInputElement;
const senhaInput = document.getElementById('senha') as HTMLInputElement;


if (confirmarSenhaInput instanceof HTMLInputElement && senhaInput instanceof HTMLInputElement) {
    function validarSenha(): void {
        if (confirmarSenhaInput.value !== senhaInput.value) {
            confirmarSenhaInput.setCustomValidity('Senhas não coincidem.');
        } else {
            confirmarSenhaInput.setCustomValidity('');
        }
    }

    senhaInput.addEventListener('input', validarSenha);
    confirmarSenhaInput.addEventListener('input', validarSenha);
}