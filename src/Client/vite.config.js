import { defineConfig } from 'vite'
import react from '@vitejs/plugin-react'
import Inspect from "vite-plugin-inspect"


// https://vite.dev/config/
export default defineConfig({
  build: {
    outDir: "../../deploy/public",
  },
  plugins: [
    Inspect(),
    react({ include: /\.(fs|js|jsx|ts|tsx)$/, jsxRuntime: "automatic" })
  ],
})

