import React, { useState, useEffect } from 'react';
import { Row, Col, Empty } from 'antd';
import NoteCard from '../Components/NoteCard';
import { INote } from '../Models/INote';

interface FavoritesPageProps {
    isDarkTheme: boolean;
}

const FavoritesPage: React.FC<FavoritesPageProps> = ({ isDarkTheme }) => {
    const [favoriteNotes, setFavoriteNotes] = useState<INote[]>([]);
    const [loading, setLoading] = useState(true);

    useEffect(() => {
        const fetchFavorites = async () => {
            try {
                // Здесь будет реальный запрос к API
                const mockNotes: INote[] = [
                    // {
                    //     id: '2',
                    //     title: 'Вторая заметка',
                    //     content: 'Это избранная заметка',
                    //     isFavorite: true,
                    //     isDeleted: false,
                    //     createdAt: new Date(),
                    //     updatedAt: new Date(),
                    // }
                ];
                setFavoriteNotes(mockNotes);
            } catch (error) {
                console.error('Ошибка при загрузке избранного:', error);
            } finally {
                setLoading(false);
            }
        };

        fetchFavorites();
    }, []);

    const handleEditNote = (id: string) => {
        console.log('Редактирование заметки:', id);
    };

    const handleToggleFavorite = (id: string) => {
        setFavoriteNotes(favoriteNotes.map(note =>
            note.id === id ? { ...note, isFavorite: !note.isFavorite } : note
        ));
    };

    const handleDeleteNote = (id: string) => {
        setFavoriteNotes(favoriteNotes.map(note =>
            note.id === id ? { ...note, isDeleted: true } : note
        ));
    };

    if (loading) return <div>Загрузка...</div>;

    return (
        <div style={{
            padding: 24,
            minHeight: 'calc(100vh - 64px - 70px)', // Высота минус хедер и футер
            display: 'flex',
            flexDirection: 'column'
        }}>
            {favoriteNotes.length > 0 ? (
                <Row gutter={[16, 16]}>
                    {favoriteNotes.map(note => (
                        <Col key={note.id}>
                            <NoteCard
                                note={note}
                                onEdit={handleEditNote}
                                onDelete={handleDeleteNote}
                                isDarkTheme={isDarkTheme}
                            />
                        </Col>
                    ))}
                </Row>
            ) : (
                <div style={{
                    flex: 1,
                    display: 'flex',
                    justifyContent: 'center',
                    alignItems: 'center'
                }}>
                <Empty description="Нет избранных заметок" />
                </div>
            )}
        </div>
    );
};

export default FavoritesPage;