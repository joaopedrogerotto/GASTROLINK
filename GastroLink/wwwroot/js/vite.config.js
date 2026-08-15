import { defineConfig } from "vite";
export default defineConfig({
    build: {
        outDir: "wwwroot/js",
        emptyOutDir: false,
        rollupOptions: {
            input: "TypeScript/dashboard.ts",
            output: {
                entryFileNames: "dashboard.js"
            }
        }
    }
});
//# sourceMappingURL=vite.config.js.map