// src/Components/CreateNoteModal.tsx
import React, { useState } from 'react';
import {Modal, Input, Form, Button, Alert} from 'antd';

interface CreateNoteModalProps {
    visible: boolean;
    onCreate: (title: string, content: string) => void;
    onCancel: () => void;
}

const CreateNoteModalComponent: React.FC<CreateNoteModalProps> = ({ visible, onCreate, onCancel }) => {
    const [form] = Form.useForm();
    const [loading, setLoading] = useState(false);
    const [error, setError] = useState<string | null>(null);

    const handleOk = () => {
        setError(null);
        form
            .validateFields()
            .then(async (values) => {
                setLoading(true);
                try {
                    onCreate(values.title, values.content);
                    form.resetFields();
                } catch (e: any) {
                    setError(e.message || 'Не удалось создать заметку');
                } finally {
                    setLoading(false);
                }
            })
            .catch((info) => {
                console.log('Validation Failed:', info);
            });
    };

    const handleCancel = () => {
        form.resetFields();
        setError(null);
        setLoading(false);
        onCancel();
    };

    return (
        <Modal
            title="Создание заметки"
            open={visible}
            onOk={handleOk}
            onCancel={handleCancel}
            okText="Создать"
            cancelText="Отмена"
            confirmLoading={loading}
        >
            {error && (
                <Alert
                    message="Ошибка"
                    description={error}
                    type="error"
                    showIcon
                    closable
                    style={{ marginBottom: 16 }}
                    onClose={() => setError(null)}
                />
            )}
            <Form layout="vertical" form={form}>
                <Form.Item
                    name="title"
                    label="Заголовок"
                    rules={[{ required: true, message: 'Введите заголовок' }]}
                >
                    <Input disabled={loading} />
                </Form.Item>
                <Form.Item
                    name="content"
                    label="Содержимое"
                    rules={[{ required: true, message: 'Введите содержимое' }]}
                >
                    <Input.TextArea rows={4} disabled={loading} />
                </Form.Item>
            </Form>
        </Modal>
    );
};

export default CreateNoteModalComponent;
