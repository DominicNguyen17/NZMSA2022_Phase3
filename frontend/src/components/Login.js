import * as React from 'react';
import Box from '@mui/material/Box';
import TextField from '@mui/material/TextField';
import { Container, Paper, Button, Link } from '@mui/material';
import './Form.css'
import axios from "axios";
import { Notifications } from 'react-push-notification';
import addNotification from 'react-push-notification';
import { useNavigate } from 'react-router-dom'
import { GoogleLogin } from 'react-google-login';
import { gapi } from 'gapi-script'

const client_id = "403386063884-pj4517kti2kpm9vua9ppirm8alrnikja.apps.googleusercontent.com"
const baseURL = "https://localhost:8080/api";

export default function Login() {
    const paperStyle = {padding:'50px 20px', width:600, margin:'20px auto', background: '#74bff1'}
    const [userName,setName] = React.useState('')
    const [password,setPassword] = React.useState('')
    const [students,setStudents]=React.useState([])
    let navigate = useNavigate()

    React.useEffect(() => {
        axios.get(baseURL + "/GetStudents/").then((response) => {
            setStudents(response.data);
        });
    }, []);

    React.useEffect(() => {
        gapi.load("client:auth2", () => {
            gapi.auth2.init({ client_id: client_id})
        })
    }, []);

    const onSuccess = (res) =>{
            registerWithGoogle(res.profileObj.name, '');
            console.log("Login success! Current user:", res.profileObj.name);
            navigate("/student");
            axios.get(baseURL + "/GetStudents/").then((response) => {
                setStudents(response.data);
            });
            
    }
    const onFailure = (res) =>{
        console.log("Login failed! res: ", res);
    }


    function warningNotification (){
        addNotification({
          title: 'Warning',
          subtitle: 'Username or Password not incorrect.',
          message: 'Please try again',
          theme: 'red',
          closeButton:"X"
        })
      };
      
      function successNotification (){
        addNotification({
          title: 'Success',
          subtitle: 'Successfully login',
          message: 'Welcome to Student App',
          theme: 'light',
          closeButton:"X",
          backgroundTop:"green",
          backgroundBottom:"yellowgreen"
        })
      };


    function Login(){
        for (let i = 0; i < students.length; i++) {
            if(userName === students[i].userName && password ===students[i].password){
                successNotification();
                navigate("/student");
            }
        };
    };

    function registerWithGoogle(userName, password) {
        axios
            .post(baseURL + "/Register/", {
                userName: userName,
                password: password,
            })
            .then((response) => {
                console.log(response);
                if (response.data === "User successfully registered."){
                    navigate("/student");
                }
            });
    }

  return (
    <Container class="loginForm">
        <Box
        component="form"
        sx={{
            '& > :not(style)': { m: 1, width: '25px' },
        }}
        noValidate
        autoComplete="off"
        >
            <Paper elevation={3} style={paperStyle}>
                <h1 class = "title" style={{color:"black"}}>Login</h1>

                <div>
                    <TextField class="inputContainer" id="outlined-basic" label="User Name" variant="outlined" fullWidth margin="normal"
                        value={userName}
                        onChange={e => setName(e.target.value)}
                        />
                        <TextField class="inputContainer" id="outlined-basic" label="Password" variant="outlined" fullWidth margin="normal"
                        value={password}
                        onChange={e => setPassword(e.target.value)} 
                        />
                    {/* <Button class="submitBtn" variant="contained" color = "secondary" onClick={() => {   
                            Login();
                        }}>
                        Submit
                    </Button> */}
                    <div style={{padding: '10px'}}>
                        <a href = "" onClick={() => {   
                            Login();
                        }}>
                        <span>Submit</span>
                        <div class="liquid"></div>
                    </a>
                    </div>
                    <div style={{padding: '10px'}}>
                        <a href = "" onClick={() => {   
                                navigate("/signup");
                            }}>
                            <span>Create An Account</span>
                            <div class="liquid"></div>
                        </a>
                    </div>
                </div>
                    
                <div id="signInGoogleButton" >
                    <GoogleLogin
                        clientId = {client_id}
                        buttonText="Login"
                        onFailure = {onFailure}
                        cookiePolicy = {'single_host_login'}
                        isSignedIn = {true}
                        onSuccess = {onSuccess}
                    />
                </div>
            </Paper>
        </Box>
    </Container>
  );
}

