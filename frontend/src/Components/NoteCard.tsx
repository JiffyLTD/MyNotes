import React, {useState} from 'react';
import {Card, Flex, Popconfirm} from 'antd';
import {
    EditOutlined,
    DeleteOutlined,
    SearchOutlined,
    ReloadOutlined,
    StarFilled,
    StarOutlined
} from '@ant-design/icons';
import {INote} from '../Models/INote';
import {Meta} from "antd/es/list/Item";
import {Typography, Skeleton } from 'antd';

const {Text} = Typography;

interface NoteCardProps {
    note?: INote;
    isNew?: boolean;
    onCreate?: () => void;
    onEdit?: (id: string, title: string, content: string) => void;
    onOpen?: (id: string) => void;
    onDelete?: (id: string) => void;
    onRestore?: (id: string) => void;
    onFavorite?: (id: string) => void;
    isDarkTheme: boolean;
}

const NoteCard: React.FC<NoteCardProps> = ({
                                               note,
                                               isNew = false,
                                               onCreate,
                                               onEdit,
                                               onOpen,
                                               onDelete,
                                               onRestore,
                                               onFavorite,
                                               isDarkTheme
                                           }) => {

    const truncate = (text: string, maxLength: number): string => {
        return text.length > maxLength ? text.slice(0, maxLength) + '...' : text;
    };

    const [imageLoaded, setImageLoaded] = useState(false);

    if (isNew) {
        return (
            <Card
                style={{ width: 300 }}
                cover={
                    <>
                        {!imageLoaded && <Skeleton.Image style={{ width: "100%", height: 200 }} active />}
                        <img
                            alt="example"
                            src="http://localhost:9000/default-images/defaultCardImage.jpg"
                            style={{
                                display: imageLoaded ? "block" : "none",
                                width: "100%",
                                height: 200,
                                objectFit: "cover"
                            }}
                            onLoad={() => setImageLoaded(true)}
                        />
                    </>
                }
                actions={["Создать новую заметку"]}
                onClick={() => onCreate()}
            >
                <Meta
                    title="Пример заголовка заметки"
                    description={
                        <>
                            <div style={{ marginBottom: 8 }}>Пример содержания заметки</div>
                            <Text type="secondary" style={{ fontSize: 12 }}>
                                Обновлено: {new Date().toLocaleString()}
                            </Text>
                        </>
                    }
                />
            </Card>
        );
    }

    return (
        <Card
            style={{width: 300}}
            cover={
                <>
                    {!imageLoaded && <Skeleton.Image style={{ width: "100%", height: 200 }} active />}
                    <img
                        alt="example"
                        src={note.imageName == null
                            ? "http://localhost:9000/default-images/defaultCardImage.jpg"
                            : `http://localhost:9000/default-images/${note.imageName}`
                    }
                        style={{
                            display: imageLoaded ? "block" : "none",
                            width: "100%",
                            height: 200,
                            objectFit: "cover"
                        }}
                        onLoad={() => setImageLoaded(true)}
                    />
                </>
            }
            actions={
                onRestore
                    ? [
                        <ReloadOutlined key="restore" onClick={() => onRestore?.(note!.id)}/>
                    ]
                    : [
                        <SearchOutlined key="open" onClick={() => onOpen?.(note!.id)}/>,
                        <EditOutlined key="edit" onClick={() => onEdit?.(note!.id, note!.title, note!.content)}/>,
                        <Popconfirm
                            key="delete"
                            title="Удалить заметку?"
                            description="Вы уверены, что хотите удалить эту заметку?"
                            onConfirm={() => onDelete?.(note!.id)}
                            okText="Да"
                            cancelText="Отмена"
                        >
                            <DeleteOutlined/>
                        </Popconfirm>
                    ]
            }
        >
            <Meta
                title={
                    <Flex justify="space-between" align="center">
                        <span>{truncate(note!.title, 24)}</span>
                        {!onDelete ? <></>
                            :
                            <span
                                onClick={() => onFavorite(note!.id)}
                                style={{cursor: 'pointer'}}
                            >
                                {note!.isFavorite ? <StarFilled style={{color: '#fadb14'}}/> : <StarOutlined/>}
                            </span>
                        }
                    </Flex>
                }
                description={
                    <>
                        <div style={{marginBottom: 8}}>{truncate(note!.content, 24)}</div>
                        <Text type="secondary" style={{fontSize: 12}}>
                            Обновлено: {new Date(note!.updatedAt).toLocaleString()}
                        </Text>
                    </>
                }
            />
        </Card>
    );
};

export default NoteCard;