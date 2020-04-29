import React, { useState, useEffect } from 'react';
import '../../App.css';
import { NavBar } from '../../features/nav/NavBar';
import { Container, Grid, Card } from 'semantic-ui-react';
import { LoadingComponent } from './LoadingComponent';
import axios from 'axios';
import { IReservation } from '../models/reservation';
import { ReservationForm } from '../../features/reservations/ReservationForm';
import { ToastContainer, toast } from 'react-toastify';
import 'react-toastify/dist/ReactToastify.css';
import { IOrder } from '../models/order';
import { StockChecker } from '../../features/stocks/StockChecker';
import { UpdatingHerd } from '../../features/herd/UpdatingHerd';

toast.configure();

const App = () => {
  const [reservations, setReservations] = useState<IReservation[]>([]);
  const [reservation, setSelectedReservation] = useState<IReservation | null>(null);
  const [loading, setLoading] = useState(true);
  const [editMode, setEditMode] = useState(false);
  const [submitting, setSubmitting] = useState(false);

  const [stock, setStock] = useState<IOrder | null>(null);
  const [updatingHerd, setUpdatingHerd] = useState(false);

  const handleOpenCreateForm = () => {
    setUpdatingHerd(false);
    setEditMode(true);

  }

  const handleOpenHome = () => {
    setEditMode(false);
    setUpdatingHerd(false);
    loadReservationOrder();
  }

  const handleOpenUpdateHerd = () => {
    setEditMode(false);
    setUpdatingHerd(true);
    loadReservationOrder();
  }

  const handleGetAvailableStock = (day: number) => {
    setSubmitting(true);
    axios.get('http://localhost:5000/yak-shop/stock/' + day)
      .then(response => {
        setStock(response.data);
        var order: IOrder = {
          Milk: response.data["milk"],
          Skins: response.data["skins"]
        };
        setStock(order);
      }).then(() => setLoading(false));
    setSubmitting(false);
  }

  const handleCreateReservation = (reservation: IReservation) => {
    setSubmitting(true);
    axios.post<IReservation>('http://localhost:5000/yak-shop/order/' + reservation.Day, reservation).then((response) => {
      setReservations([...reservations, reservation]);
      setEditMode(false);
      if (response.status === 201) {
        toast.success("Your order submitted", {
          position: toast.POSITION.BOTTOM_RIGHT,
          autoClose: 5000
        });
      }
      if (response.status === 206) {
        var warnMessage = "Your order partially submitted, only ";
        var order: any = response.data;
        for (var key in order) {
          warnMessage += key + " ";
        }
        warnMessage += "is submitted";
        toast.warn(warnMessage, {
          position: toast.POSITION.BOTTOM_RIGHT,
          autoClose: 5000
        });
      }
    }).catch((error) => {
      toast.error("We are out of stock for that order", {
        position: toast.POSITION.BOTTOM_RIGHT,
        autoClose: 5000
      });
    }).then(() => {
      axios.get<IReservation[]>('http://localhost:5000/yak-shop/order')
        .then(response => {
          let reservations: IReservation[] = [];
          response.data.forEach(reservation => {
            reservations.push(reservation);
          });
          setReservations(reservations.sort((a, b) => (a.Day < b.Day) ? 1 : -1));
        }).then(() => setLoading(false));
      setSubmitting(false)
    });

  }

  const loadReservationOrder = () => {
    axios.get<IReservation[]>('http://localhost:5000/yak-shop/order')
      .then(response => {
        let reservations: IReservation[] = [];
        response.data.forEach(reservation => {
          reservations.push(reservation);
        });
        setReservations([...reservations.sort((a, b) => (a.Day < b.Day) ? 1 : -1)]);
      }).then(() => setLoading(false));
  }

  useEffect(() => {
    loadReservationOrder();

  }, []);

  if (loading) { return <LoadingComponent content="loading in moments" /> }


  return (
    <div className="App">
      <NavBar openHome={handleOpenHome} openCreateForm={handleOpenCreateForm} openUpdateHerd={handleOpenUpdateHerd} />
      {!updatingHerd && (<Container style={{ marginTop: '6em' }} >
        <Grid>
          <Grid.Column width={2}>
          </Grid.Column>
          <Grid.Column width={6}>
            {reservations.map((reservation: IReservation) => (
              <Card fluid key={reservation.Id}>
                <Card.Content>
                  <Card.Header>
                    <i className="shopping basket icon"></i>
                    {reservation.Customer}
                  </Card.Header>
                  <Card.Description style={{ marginBottom: '0.5em' }}>
                    {reservation.Day} days later
                  </Card.Description>
                  <Card.Meta>
                    {reservation.Order.Milk > 0 && (<p>Milk:{reservation.Order.Milk}</p>)}
                    {reservation.Order.Skins > 0 && (<p>Skins:{reservation.Order.Skins}</p>)}
                  </Card.Meta>
                </Card.Content>
              </Card>
            ))}
            {reservations.length === 0 && <div>No reservation</div>}
          </Grid.Column>
          <Grid.Column width={6}>
            {!updatingHerd && (<StockChecker getAvailableStock={handleGetAvailableStock} submitting={submitting} stock={stock!} />)}
            {editMode && (
              <ReservationForm
                reservation={reservation}
                setEditMode={setEditMode}
                createReservation={handleCreateReservation}
                submitting={submitting} />)}
          </Grid.Column>
          <Grid.Column width={2}>
          </Grid.Column>
        </Grid>

      </Container>)}
      {updatingHerd && <UpdatingHerd submitting={submitting} />}
      <ToastContainer autoClose={8000} />


    </div>

  );

};

export default App;
