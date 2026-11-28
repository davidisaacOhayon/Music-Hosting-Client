import { app, BrowserWindow, ipcMain, IpcMainEvent } from 'electron';
import started from 'electron-squirrel-startup';
import path from "node:path"
import { HttpMediaSearchResponse, MediaRequest } from "./types/http-request-type/httptypes";
import { httpRequestMedia } from './services/http-service/HttpService';
import { MediaIPCChannel, RegisterIPCChannels } from './services';
const INDEX_FILE_PATH: string = process.cwd() + "/src/index.html";

declare const MAIN_WINDOW_WEBPACK_ENTRY: string;
declare const MAIN_WINDOW_PRELOAD_WEBPACK_ENTRY: string;
// Handle creating/removing shortcuts on Windows when installing/uninstalling.
if (started) {
  app.quit();
}


const initializeIPCListeners = () => {
  console.log("Registering IPC channels")
   const mediaIpc : MediaIPCChannel = new MediaIPCChannel();
   mediaIpc.register();
}

const createWindow = (): void => {
  // Create the browser window.
  const mainWindow = new BrowserWindow({
    height: 600,
    width: 800,
    webPreferences: {
      preload: MAIN_WINDOW_PRELOAD_WEBPACK_ENTRY,
      contextIsolation: true,
      nodeIntegration: false,
      sandbox: false
    },
 
  });
mainWindow.webContents.on(
  'preload-error',
  (_event, preloadPath, error) => {
    console.error('PRELOAD PATH:', preloadPath);
    console.error('PRELOAD ERROR:', error);
  }
);
  mainWindow.loadURL(MAIN_WINDOW_WEBPACK_ENTRY);
 

  // Open the DevTools.
  mainWindow.webContents.openDevTools();
};

// initializeIPCListeners();

  ipcMain.on("media:search", async (_event, query: string) => {
    console.log(`I got your ${query}`);
    const request : MediaRequest = {
      songName: query
    }

    const response : HttpMediaSearchResponse | null = await httpRequestMedia(request);
    if(response) {
      console.log(response.results);
    }


  })

// This method will be called when Electron has finished
// initialization and is ready to create browser windows.
// Some APIs can only be used after this event occurs.
app.whenReady().then(() => {
  createWindow();

  // On OS X it's common to re-create a window in the app when the
  // dock icon is clicked and there are no other windows open.
  app.on('activate', () => {
    if (BrowserWindow.getAllWindows().length === 0) {
      createWindow();
    }
  });
});

// Quit when all windows are closed, except on macOS. There, it's common
// for applications and their menu bar to stay active until the user quits
// explicitly with Cmd + Q.
app.on('window-all-closed', () => {
  if (process.platform !== 'darwin') {
    app.quit();
  }
});

// In this file you can include the rest of your app's specific main process
// code. You can also put them in separate files and import them here.
