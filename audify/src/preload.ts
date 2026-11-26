//oxlint-disable eslint-plugin-unicorn/no-empty-file
// See the Electron documentation for details on how to use preload scripts:
// https://www.electronjs.org/docs/latest/tutorial/process-model#preload-scripts


const {contextBridge, ipcRenderer} = require("electron");


const something: string = "Random"
console.log("Preload is running");
contextBridge.exposeInMainWorld('electronAPI', {
  setTitle: (title: string) => ipcRenderer.send('set-title', title),
  searchMedia: (query: string) => ipcRenderer.send("media:search", query)
})  
console.log("electronAPI exposed");