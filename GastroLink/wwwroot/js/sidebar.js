"use strict";
document.addEventListener('DOMContentLoaded', function () {
    const sidebar = document.getElementById('sidebar');
    const toggle = document.getElementById('sidebarToggle');
    const backdrop = document.getElementById('sidebarBackdrop');
    if (!sidebar || !toggle || !backdrop) {
        return;
    }
    function closeSidebar() {
        sidebar.classList.remove('is-open');
        toggle.setAttribute('aria-expanded', 'false');
    }
    toggle.addEventListener('click', function () {
        const isOpen = sidebar.classList.toggle('is-open');
        toggle.setAttribute('aria-expanded', isOpen ? 'true' : 'false');
    });
    backdrop.addEventListener('click', closeSidebar);
    const groupToggles = document.querySelectorAll('.nav-group-toggle');
    groupToggles.forEach(function (btn) {
        btn.addEventListener('click', function () {
            const group = btn.closest('.nav-group');
            if (!group) {
                return;
            }
            const wasOpen = group.classList.contains('is-open');
            document.querySelectorAll('.nav-group.is-open').forEach(function (g) {
                g.classList.remove('is-open');
            });
            if (!wasOpen) {
                group.classList.add('is-open');
            }
        });
    });
    const jaMarcado = new Set();
    const navLinks = document.querySelectorAll('.nav-group a, .nav-single a');
    navLinks.forEach(function (link) {
        if (link.href === window.location.href && !jaMarcado.has(link.href)) {
            link.classList.add('is-active');
            jaMarcado.add(link.href);
            const group = link.closest('.nav-group');
            if (group) {
                group.classList.add('is-open');
            }
        }
    });
});
//# sourceMappingURL=sidebar.js.map