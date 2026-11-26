 
export type MediaRequest = {
    songName: string
}

export type MediaMetaData = {
    id: string,
    title: string,
    url: string,
}

export type HttpMediaSearchResponse = {
    results: MediaMetaData[]
}