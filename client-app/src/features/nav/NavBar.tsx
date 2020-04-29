import React from 'react';
import logo from '../../logo.svg';
import { Menu, Container, Button } from 'semantic-ui-react';

interface IProps {
    openCreateForm: () => void;
    openUpdateHerd: () => void;
    openHome: () => void;
}

export const NavBar: React.FC<IProps> = ({ openCreateForm, openUpdateHerd, openHome }) => {
    return (
        <Menu fixed='top' inverted>
            <Container>
                <Menu.Item header onClick={openHome}>
                    <img src={logo} className="App-logo" alt="logo" style={{ marginRight: 10, height: 20 }} />
                        Yak Shop
                    </Menu.Item>
                <Menu.Item>
                    <Button positive content="Send order" onClick={openCreateForm} />
                </Menu.Item>
                <Menu.Item>
                    <Button secondary content="Update herd" onClick={openUpdateHerd} />
                </Menu.Item>
            </Container>
        </Menu>
    )
}
