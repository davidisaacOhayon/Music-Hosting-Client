import type { IPCChannel } from "../ipcChannelRegister"
import {ipcMain} from 'electron';
import {httpRequestMedia} from '../../http-service/HttpService';
import { MediaRequest, HttpMediaSearchResponse } from "../../../types";
import { useSongListStore } from "../../../stores/SongListContentStore/useSongListStore";

export class MediaIPCChannel implements IPCChannel  {

    register = () => {
        ipcMain.handle("media:search", async (_, query) => this.handleMediaSearch(query))
    }

    handleMediaStream = async (mediaId: string) => {
        // TODO: Implement request to .NET backend to send media stream.
    }
    
    handleMediaSearch = async (query: string) => {
        const setSongList = useSongListStore.getState().setSongList;
        const mediaQuery: MediaRequest = {
            songName: query
        }
        const response : HttpMediaSearchResponse | null = await httpRequestMedia(mediaQuery);

        if (response) {
            setSongList(response.results);
        }
    }
}

 