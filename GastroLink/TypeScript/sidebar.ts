document.addEventListener('DOMContentLoaded', function (): void {
    const sidebar = document.getElementById('sidebar');
    const toggle = document.getElementById('sidebarToggle');
    const backdrop = document.getElementById('sidebarBackdrop');

    if (!sidebar || !toggle || !backdrop) {
        return;
    }

    function closeSidebar(): void {
        sidebar!.classList.remove('is-open');
        toggle!.setAttribute('aria-expanded', 'false');
    }

    toggle.addEventListener('click', function (): void {
        const isOpen = sidebar.classList.toggle('is-open');
        toggle.setAttribute('aria-expanded', isOpen ? 'true' : 'false');
    });

    backdrop.addEventListener('click', closeSidebar);

    const groupToggles = document.querySelectorAll<HTMLButtonElement>('.nav-group-toggle');

    groupToggles.forEach(function (btn) {
        btn.addEventListener('click', function () {
            const group = btn.closest<HTMLLIElement>('.nav-group');
            if (!group) {
                return;
            }

            const wasOpen = group.classList.contains('is-open');

            document.querySelectorAll<HTMLLIElement>('.nav-group.is-open').forEach(function (g) {
                g.classList.remove('is-open');
            });

            if (!wasOpen) {
                group.classList.add('is-open');
            }
        });
    });

    const jaMarcado = new Set<string>();

    const navLinks = document.querySelectorAll<HTMLAnchorElement>('.nav-group a, .nav-single a');

    navLinks.forEach(function (link) {
        if (link.href === window.location.href && !jaMarcado.has(link.href)) {
            link.classList.add('is-active');
            jaMarcado.add(link.href);

            const group = link.closest<HTMLLIElement>('.nav-group');
            if (group) {
                group.classList.add('is-open');
            }
        }
    });
});