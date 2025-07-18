import { defineConfig } from 'vite'
import react from '@vitejs/plugin-react'

// https://vitejs.dev/config/
export default defineConfig({
  plugins: [react()],
  server: {
    port: 3000,
    open: true
  },
  build:{
    outDir: "../wwwroot",
    emptyOutDir: true,
    rollupOptions: {
      output: {
        // Disable hashing for entry files (e.g., app.js)
        entryFileNames: `[name].js`,
        // Disable hashing for chunk files (e.g., vendor chunks)
        chunkFileNames: `[name].js`,
        // Disable hashing for static assets (e.g., images, CSS)
        assetFileNames: `[name].[ext]`,
      },
    },
  },
})
