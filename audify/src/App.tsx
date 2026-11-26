import React from 'react';
import {createRoot} from 'react-dom/client';
import MainContainer from './components/MainContainer/MainContainer';


const rootElement: HTMLElement | null = document.getElementById("root");

if (!rootElement){ 
    throw new Error("No root element found")
}
const root = createRoot(rootElement);
root.render(<MainContainer />);