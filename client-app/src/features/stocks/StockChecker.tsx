import React, { FormEvent, useState } from 'react'
import { Input, Segment, Form, Button } from 'semantic-ui-react'
import { IOrder } from '../../app/models/order';

interface IProps {
    getAvailableStock: (day: number) => void;
    submitting: boolean;
    stock: IOrder
}


export const StockChecker: React.FC<IProps> = ({ getAvailableStock, submitting, stock }) => {

    const [day, setDay] = useState(0);

    const handleSubmit = () => {
        getAvailableStock(day);
    }

    const handleInputChange = (event: FormEvent<HTMLInputElement | HTMLTextAreaElement>) => {
        setDay(parseInt(event.currentTarget.value));
    }

    return (
        <Segment clearing>
            <Form onSubmit={handleSubmit}>
                <Form.Field>
                    <label>Check available stocks for a specific day </label>
                    <Input name='Customer' onChange={handleInputChange} placeholder='Day'></Input>
                </Form.Field>
                {stock && (
                    <div>
                        <Form.Field>
                            <label>Milk</label>
                            <label>{stock.Milk}</label>
                        </Form.Field>
                        <Form.Field>
                            <label>Skin</label>
                            <label>{stock.Skins}</label>
                        </Form.Field>
                    </div>)}
                <Button loading={submitting} floated='right' positive type='submit' content='Check'></Button>
            </Form>
        </Segment>
    )
}
