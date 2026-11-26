
import {create} from 'zustand';
import type {MediaMetaData} from "../../types";



interface SongPlayerStore {
    currentSong: MediaMetaData | null,
    setCurrentSong: (currentSong: MediaMetaData | null) => void
}


export const useSongPlayerStore = create<SongPlayerStore>((set) => ({
    currentSong: null,
    setCurrentSong: (currentSong: MediaMetaData | null) => {
        set(() => ({currentSong}))
    }
}))