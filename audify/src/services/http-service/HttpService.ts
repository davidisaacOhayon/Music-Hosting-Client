import axios from 'axios';
import type {MediaRequest, HttpMediaSearchResponse} from '../../types/http-request-type/httptypes';
 
// AxiosRequestConfig<D = any, P = any>
// RawAxiosRequestConfig<D = any, P = any>
// InternalAxiosRequestConfig<D = any, P = any>
// AxiosDefaults<D = any, P = any>
// CreateAxiosDefaults<D = any, P = any>

// AxiosResponse<T = any, D = any, H = {}, P = any>
// AxiosPromise<T = any, D = any, P = any>
// AxiosError<T = unknown, D = any, P = any>
// CanceledError<T, D = any, P = any>



export const httpRequestMedia = async (query: MediaRequest) : Promise<HttpMediaSearchResponse | null> => {
    console.log("calling request for");
    console.log(query.songName);
    let data: HttpMediaSearchResponse;
    try{
        data = await axios.post('http://localhost:5000/Client/SearchSong', {
            songName: query.songName
        })
        .then(response => {if (response.data) {
            // TODO: Proper Error handling
            return response.data;
        }})

        return data;
       
    } catch (error) {
        console.log();
        return null
    }

    }
