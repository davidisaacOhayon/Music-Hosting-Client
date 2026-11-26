import {create} from 'zustand';
import type {MediaMetaData} from '../../types/http-request-type/httptypes';


interface SongListStore {
    songList: MediaMetaData[];
    setSongList: (songList: MediaMetaData[]) => void;
}


export const useSongListStore = create<SongListStore>((set) => ({
    songList: [],
    setSongList: (songList: MediaMetaData[]) => {
        set(() => ({songList}));
    }

}))

