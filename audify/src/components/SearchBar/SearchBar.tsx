
import searchLogo from "../../assets/components/searchbutton/search.svg";
import "./searchbar.css";
import {useState} from 'react';
export default function SearchBar(){ 

    const [search, setSearch] = useState("");

    const handleSearch = async () => {

        //TODO
        const result = await window.electronAPI.searchMedia(search);
        console.log(window.electronAPI);
        
    }



    return(
        <div className="search-bar-div">
            <button id="search-button" onClick={() => handleSearch()}>
                <img src={searchLogo}></img>
            </button>
            <input onChange={(self) => setSearch(self.target.value) }type="text" name="" placeholder="Enter a song to search."></input>
        </div>

    )



}