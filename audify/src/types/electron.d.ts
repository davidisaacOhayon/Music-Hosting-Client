export {};


declare global {
    interface Window{
        electronAPI: {
            searchMedia: (query: string) => Promise<any>;
        }
    }
}