import { IOrder } from "./order";

export interface IReservation {
    Id: string;
    Day: number;
    Customer: string;
    Order: IOrder;
}