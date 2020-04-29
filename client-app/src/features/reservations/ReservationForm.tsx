import React, { useState, FormEvent } from 'react';
import { Segment, Form, Button, Input } from 'semantic-ui-react';
import { IReservation } from '../../app/models/reservation';

interface IProps {
    setEditMode: (editMode: boolean) => void;
    reservation: IReservation | null;
    createReservation: (reservation: IReservation) => void;
    submitting: boolean;
}

export const ReservationForm: React.FC<IProps> = ({ setEditMode, reservation: initialFormState, createReservation, submitting }) => {

    const initializeForm = () => {
        if (initialFormState) {
            return initialFormState;
        }
        else {
            return {
                Id: '',
                Day: 0,
                Customer: '',
                Order: {
                    Milk: 0,
                    Skins: 0
                }
            }
        }
    };

    const [reservation, setReservation] = useState<IReservation>(initializeForm);

    const handleSubmit = () => {
        let newReservation = {
            ...reservation
        };

        createReservation(newReservation);
    }

    const handleInputChange = (event: FormEvent<HTMLInputElement | HTMLTextAreaElement>) => {
        const { name, value } = event.currentTarget;
        if (name.startsWith('Order')) {
            if (name === 'Order.Milk') reservation.Order.Milk = parseFloat(value);
            if (name === 'Order.Skins') reservation.Order.Skins = parseInt(value);
        }
        else {
            setReservation({ ...reservation, [name]: value });
        }
    }

    return (
        <div>
            <Segment clearing>
                <Form onSubmit={handleSubmit}>
                    <Form.Field>
                        <label>Customer Name</label>
                        <Input name='Customer' value={reservation.Customer} onChange={handleInputChange} placeholder='Customer name'></Input>
                    </Form.Field>
                    <Form.Field>
                        <label>Days later</label>
                        <Input name='Day' type='number' onChange={handleInputChange} placeholder='Day'></Input>
                    </Form.Field>
                    <Form.Field>
                        <label>Liters of Milk</label>
                        <Input name='Order.Milk' onChange={handleInputChange} placeholder='Milk'></Input>
                    </Form.Field>
                    <Form.Field>
                        <label>Skins</label>
                        <Input name='Order.Skins' onChange={handleInputChange} placeholder='Skins'></Input>
                    </Form.Field>
                    <Button loading={submitting} floated='right' positive type='submit' content='Submit'></Button>
                    <Button loading={submitting} floated='right' type='button' content='Cancel' onClick={() => { setEditMode(false) }}></Button>
                </Form>
            </Segment>
        </div>
    )
}
