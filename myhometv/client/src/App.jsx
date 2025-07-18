import { Route, Routes } from "react-router-dom";

import APIManagerPage from "./modules/admin/pages/APIManagerPage";
import CamerasPage from "./modules/admin/pages/CamerasPage";
import LocalHeadlinesPage from "./modules/admin/pages/LocalHeadlinesPage";
import Main from "./modules/main/Main";
import NavBar from "./modules/admin/NavBar";
import NewsPage from "./modules/admin/pages/NewsPage";
import SportsPage from "./modules/admin/pages/SportsPage";
import WeatherPage from "./modules/admin/pages/WeatherPage";

import './index.css';

function App() {
  return (
    <Routes>
      <Route path="/" element={<Main />} />
      <Route path="/admin" element={<NavBar />}>
        <Route path="" element={<WeatherPage />} />
        <Route path="localheadlines" element={<LocalHeadlinesPage />} />
        <Route path="news" element={<NewsPage />} />
        <Route path="cameras" element={<CamerasPage />} />
        <Route path="sports" element={<SportsPage />} />
        <Route path="apis" element={<APIManagerPage />} />
      </Route>
    </Routes>
  );
}

export default App;
