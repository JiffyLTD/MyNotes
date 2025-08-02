import React from 'react';
import { Routes, Route } from 'react-router-dom';
import MainPage from "../Pages/MainPage";
import NotFoundPage from "../Pages/NotFoundPage";
import PrivacyPage from "../Pages/PrivacyPage";
import TermsPage from "../Pages/TermsPage";
import FavoritesPage from "../Pages/FavoritesPage";
import DeletedPage from "../Pages/DeletedPage";

interface AppRoutesProps {
    isDarkTheme: boolean;
}

const AppRoutes: React.FC<AppRoutesProps> = ({ isDarkTheme }) => {
    return (
        <Routes>
            <Route path="/main" element={<MainPage isDarkTheme={isDarkTheme} />} />
            <Route path="/favorite" element={<FavoritesPage isDarkTheme={isDarkTheme} />} />
            <Route path="/deleted" element={<DeletedPage isDarkTheme={isDarkTheme} />} />
            <Route path="/privacy" element={<PrivacyPage isDarkTheme={isDarkTheme} />} />
            <Route path="/terms" element={<TermsPage isDarkTheme={isDarkTheme} />} />
            <Route path="/" element={<MainPage isDarkTheme={isDarkTheme} />} />
            <Route path="*" element={<NotFoundPage isDarkTheme={isDarkTheme} />} />
        </Routes>
    );
};

export default AppRoutes;