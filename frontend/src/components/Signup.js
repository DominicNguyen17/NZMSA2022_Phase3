import * as React from 'react';
import Box from '@mui/material/Box';
import TextField from '@mui/material/TextField';
import { Container, Paper, Button } from '@mui/material';
import './Form.css'
import axios from "axios";
import { Notifications } from 'react-push-notification';
import addNotification from 'react-push-notification';
import {useNavigate} from 'react-router-dom'



const baseURL = "https://localhost:8080/api";

export default function Signup() {
    const paperStyle = {padding:'50px 20px', width:600, margin:'20px auto', background: '#74bff1'}
    const [userName,setName] = React.useState('')
    const [password,setPassword] = React.useState('')
    let navigate = useNavigate()

    function warningNotification (){
        addNotification({
          title: 'Warning',
          subtitle: 'Username not available.',
          message: 'You have to enter another username',
          theme: 'red',
          closeButton:"X"
        })
      };
      
      function successNotification (){
        addNotification({
          title: 'Success',
          subtitle: 'Successfully registered',
          message: 'Welcome to Student App',
          theme: 'light',
          closeButton:"X",
          backgroundTop:"green",
          backgroundBottom:"yellowgreen"
        })
      };


    function Register() {
        axios
            .post(baseURL + "/Register/", {
                userName: userName,
                password: password,
            })
            .then((response) => {
                console.log(response);
                if (response.data === "Username not available."){
                    warningNotification();
                    navigate("/")
                }else{
                    successNotification();
                    navigate("/")
                }
            });
    }  

  return (
    <Container class="signupFrm">
        <Box
        component="form"
        sx={{
            '& > :not(style)': { m: 1, width: '25px' },
        }}
        noValidate
        autoComplete="off"
        >
            <Notifications position="top-left"/>
            <Paper elevation={3} style={paperStyle}>
                <h1 class = "title" style={{color:"black"}}>Sign up</h1>
                    <TextField class="inputContainer" id="outlined-basic" label="User Name" variant="outlined" fullWidth margin="normal"
                    value={userName}
                    onChange={e => setName(e.target.value)}
                    />
                    <TextField class="inputContainer" id="outlined-basic" label="Password" variant="outlined" fullWidth margin="normal"
                    value={password}
                    onChange={e => setPassword(e.target.value)} 
                    />
                <div style={{padding: '10px'}}>
                    <a onClick={() => {   
                            Register();
                        }}>
                        <span>Submit</span>
                        <div class="liquid"></div>
                    </a>
                </div>
            </Paper>
        </Box>
    </Container>
  );
}