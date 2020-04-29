import React, { useState } from 'react'
import { Input, Segment, Form, Button, Message } from 'semantic-ui-react'
import Axios from 'axios';
import { toast } from 'react-toastify';

interface IProps {
    submitting: boolean;
}


export const UpdatingHerd: React.FC<IProps> = ({ submitting }) => {

    const [file, setFile] = useState('');

    const handleSubmit = async (e: any) => {
        submitting = true;
        e.preventDefault();
        const formData = new FormData();
        formData.append('file', file);

        try {
            await Axios.put('http://localhost:5000/yak-shop/herd/upload', formData, {
                headers: {
                    'Content-Type': 'multipart/form-data'
                }
            }).then(() => {
                toast.success("Your herd is updated", {
                    position: toast.POSITION.BOTTOM_RIGHT,
                    autoClose: 5000
                });
            });

        } catch (err) {
            toast.error(err, {
                position: toast.POSITION.BOTTOM_RIGHT,
                autoClose: 5000
            });
        }
        submitting = false;
    };

    const handleInputChange = (e: any) => {
        setFile(e.target.files[0]);
    };

    return (
        <Segment clearing style={{ marginTop: '50px' }}>
            <Form onSubmit={handleSubmit}>
                <Form.Field>
                    <Input name='Customer' type='file' onChange={handleInputChange} placeholder='Day'></Input>
                </Form.Field>
                <Button loading={submitting} floated='right' positive type='submit' content='Submit'></Button>
            </Form>
            <Message warning style={{ marginTop: '60px' }}>
                <Message.Header>Warning for updating the herd</Message.Header>
                <p>If you override your herd, all your orders will be wiped out for safety reasons</p>
            </Message>
        </Segment>
    )
}
