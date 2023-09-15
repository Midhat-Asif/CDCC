import React from "react";
import  Counter from "./components/Counter";
import Sheet from './components/Sheet';
import FetchData  from "./components/FetchData";
import Home from "./components/Home";
import FileUpload from "./components/FileUpload";

const AppRoutes: {
  index?: boolean;
  path?: string;
  element: JSX.Element;
}[] = [
  {
    index: true,
    element: <Home />
  },
  {
    path: '/counter',
    element: <Counter />
  },
  {
    path: '/fetch-data',
    element: <FetchData />
  },
  {
    path: '/display',
    element: <Sheet />
  },
  {
    path: '/fileupload',
    element: <FileUpload />
  }
];

export default AppRoutes;
