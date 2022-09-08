import * as React from 'react';
import { Container, Paper, Button } from '@mui/material';
import './Student.css'
import axios from "axios";
import { Notifications } from 'react-push-notification';
import addNotification from 'react-push-notification';
import Modal from '@mui/material/Modal';
import Box from '@mui/material/Box';
import TextField from '@mui/material/TextField';
import { GoogleLogout } from 'react-google-login';
import { gapi } from 'gapi-script'
const baseURL = "https://localhost:8080/api";
const style = {
    position: 'absolute',
    top: '50%',
    left: '50%',
    transform: 'translate(-50%, -50%)',
    width: 400,
    bgcolor: 'background.paper',
    border: '2px solid #000',
    boxShadow: 24,
    pt: 2,
    px: 4,
    pb: 3,
  };
  const client_id = "403386063884-pj4517kti2kpm9vua9ppirm8alrnikja.apps.googleusercontent.com"
export default function Student() {
    const paperStyle = {padding:'50px 20px', width:600, margin:'20px auto'}
    const [enrolmentId, setEnrolmentId] = React.useState('')
    const [studentId, setStudentId] = React.useState('')
    const [userName,setName] = React.useState('')
    const [students,setStudents]=React.useState([])
    const [enrolments, setEnrolments] = React.useState([])
    const [subjectName,setSubjectName] = React.useState('')
    const [assignment1,setAssignment1] = React.useState('')
    const [assignment2,setAssignment2] = React.useState('')
    const [assignment3,setAssignment3] = React.useState('')
    const [test,setTest] = React.useState('')
    const [finalExam,setFinalExam] = React.useState('')
    const [enrolModalOpen, setEnrolModalOpen] = React.useState(false);
    const handleEnrolOpen = () => setEnrolModalOpen(true);
    const handleEnrolClose = () => setEnrolModalOpen(false);
    const [detailModalOpen, setDetailModalOpen] = React.useState(false);
    function handleDetailOpen(){
      setDetailModalOpen(true);
    }
    const handleDetailClose = () => setDetailModalOpen(false);
    const [updateModalOpen, setUpdateModalOpen] = React.useState(false);
    const handleUpdateOpen = () => setUpdateModalOpen(true);
    const handleUpdateClose = () => setUpdateModalOpen(false);


    React.useEffect(() => {
      gapi.load("client:auth2", () => {
          gapi.auth2.init({ client_id: client_id})
      })
  }, []);

  const onLogoutSuccess = () =>{
    console.log("Logout success!");
   
}

    function deletedSuccessNotification (){
        addNotification({
          title: 'Success',
          subtitle: 'Successfully deleted',
          theme: 'light',
          closeButton:"X",
          backgroundTop:"green",
          backgroundBottom:"yellowgreen"
        })
      };

    React.useEffect(() => {
        axios.get(baseURL + "/GetStudents/").then((response) => {
            setStudents(response.data);
        });
    }, []);

    function GetEnrolmentsOfStudent(studentId){
        axios.get(baseURL + "/GetEnrolmentsOfStudent/" + studentId).then((response) => {
          setEnrolments(response.data);
            console.log(response);
    });
    }

    

    function deleteStudent(studentId) {
        axios
            .delete(baseURL + "/DeleteStudent/" + studentId)
            .then(() => {
                deletedSuccessNotification();
            });
    }

    function deleteSubject(subjectId) {
      axios
          .delete(baseURL + "/DeleteSubject/" + subjectId)
          .then(() => {
              deletedSuccessNotification();
          });
  }

    function Enroll() {
        axios
            .post(baseURL + "/EnrollSubject/", {
                userName: userName,
                subjectName: subjectName,
            })
            .then((response) => {
                console.log(response);
            });
    } 

    function Update(enrolmentId) {
      axios
          .put(baseURL + "/UpdateSubjectOfEnrolment/" + enrolmentId, {
            assignment1: assignment1,
            assignment2: assignment2,
            assignment3: assignment3,
            test: test,
            finalExam: finalExam
          })
          .then((response) => {
            console.log(response);
          });
    }
 
  return (
    <Container>
        <Notifications position="top-left"/>
        <Paper elevation={3} style={paperStyle}>
            <h1>Students</h1>
                <table className="Table" style={{width:"100%"}}>
                    <thead>
                    <tr>
                        <th>Id</th>
                        <th>UserName</th>
                        <th>Password</th>
                        <th>Option</th>
                    </tr>
                    </thead>
                    <tbody>
                        {students.map(student=>
                            <tr key={student.id}>
                                <td>{student.id}</td>
                                <td>{student.userName}</td>
                                <td>{student.password}</td>
                                <td>
                                    <Button variant="text" onClick={handleEnrolOpen}>
                                        Enroll
                                    </Button>
                                    <Modal
                                        open={enrolModalOpen}
                                        onClose={handleEnrolClose}
                                        aria-labelledby="enrol-modal-title"
                                        aria-describedby="enrol-modal-description"
                                    >
                                        <Box sx={style}>
                                            <h2 id="enrol-modal-title">
                                              Enroll Subject
                                            </h2>
                                            <p id="enrol-modal-description">
                                                <TextField class="inputContainer" id="outlined-basic" label="Student Id" variant="outlined" fullWidth margin="normal"
                                                    value={studentId}
                                                    onChange={e => {
                                                      setStudentId(e.target.value)
                                                    }}
                                                />
                                                <TextField class="inputContainer" id="outlined-basic" label="User Name" variant="outlined" fullWidth margin="normal"
                                                value={userName}
                                                onChange={e => setName(e.target.value)}
                                                />
                                                <TextField class="inputContainer" id="outlined-basic" label="Subject Name" variant="outlined" fullWidth margin="normal"
                                                value={subjectName}
                                                onChange={e => setSubjectName(e.target.value)} 
                                                />
                                                <Button variant="text"  onClick={() => {  
                                                        Enroll();
                                                    }}>
                                                    Submit
                                                </Button>
                                            </p>
                                        </Box>
                                    </Modal>
                                    <Button onClick={() => {handleDetailOpen(); GetEnrolmentsOfStudent(student.id)}} >View Details</Button>
                                    <Modal
                                      open={detailModalOpen}
                                      onClose={handleDetailClose}
                                      aria-labelledby="details-modal-title"
                                      aria-describedby="details-modal-description"
                                    >
                                      <Box sx={{ ...style, width: 1200 }}>
                                        <h2 id="details-modal-title">Details</h2>
                                        <table className="Table" style={{width:"100%"}}>
                                          <thead>
                                          <tr>
                                              <th>Enrolment Id</th>
                                              <th>Student Id</th>
                                              <th>User Name</th>
                                              <th>Subject Id</th>
                                              <th>Subject Name</th>
                                              <th>Assignment 1</th>
                                              <th>Assignment 2</th>
                                              <th>Assignment 3</th>
                                              <th>Test</th>
                                              <th>Final Exam</th>
                                              <th>Average</th>
                                              <th>Option</th>
                                          </tr>
                                          </thead>
                                          <tbody>
                                            {enrolments.map(enrolment=>
                                              <tr key={enrolment.id}>
                                                  <td>{enrolment.id}</td>
                                                  <td>{enrolment.studentId}</td>
                                                  <td>{enrolment.userName}</td>
                                                  <td>{enrolment.subjectId}</td>
                                                  <td>{enrolment.subjectName}</td>
                                                  <td>{enrolment.assignment1}</td>
                                                  <td>{enrolment.assignment2}</td>
                                                  <td>{enrolment.assignment3}</td>
                                                  <td>{enrolment.test}</td>
                                                  <td>{enrolment.finalExam}</td>
                                                  <td>{enrolment.average}</td>
                                                  <td>
                                                    <Button variant="text" onClick={() => deleteSubject(enrolment.subjectId)}>
                                                        Delete
                                                    </Button>
                                                    </td>
                                              </tr>
                                            )}
                                          </tbody>
                                        </table>
                                        <Button onClick={handleUpdateOpen}>Update</Button>
                                        <Modal
                                          hideBackdrop
                                          open={updateModalOpen}
                                          onClose={handleUpdateClose}
                                          aria-labelledby="mark-modal-title"
                                          aria-describedby="mark-modal-description"
                                        >
                                          <Box sx={{ ...style, width: 1000 }}>
                                            <h2 id="mark-modal-title">Update Details</h2>
                                            <p id="mark-modal-description">
                                            <TextField class="inputContainer" id="outlined-basic" label="Enrolment Id" variant="outlined" fullWidth margin="normal"
                                              value={enrolmentId}
                                              onChange={e => setEnrolmentId(e.target.value)} 
                                              />
                                              <TextField class="inputContainer" id="outlined-basic" label="Assignment 1" variant="outlined" fullWidth margin="normal"
                                              value={assignment1}
                                              onChange={e => setAssignment1(e.target.value)} 
                                              />
                                              <TextField class="inputContainer" id="outlined-basic" label="Assignment 2" variant="outlined" fullWidth margin="normal"
                                              value={assignment2}
                                              onChange={e => setAssignment2(e.target.value)} 
                                              />
                                              <TextField class="inputContainer" id="outlined-basic" label="Assignment 3" variant="outlined" fullWidth margin="normal"
                                              value={assignment3}
                                              onChange={e => setAssignment3(e.target.value)} 
                                              />
                                              <TextField class="inputContainer" id="outlined-basic" label="Test" variant="outlined" fullWidth margin="normal"
                                              value={test}
                                              onChange={e => setTest(e.target.value)} 
                                              />
                                              <TextField class="inputContainer" id="outlined-basic" label="Final" variant="outlined" fullWidth margin="normal"
                                              value={finalExam}
                                              onChange={e => setFinalExam(e.target.value)} 
                                              />
                                              <Button variant="text" onClick={() => {   
                                                      Update(enrolmentId);
                                                  }}>
                                                  Submit
                                              </Button> 
                                            <Button onClick={handleUpdateClose}>Close</Button>
                                            </p>
                                          </Box>
                                        </Modal>
                                        
                                      </Box>
                                    </Modal>
                                    <Button variant="text" onClick={() => deleteStudent(student.id)}>
                                        Delete
                                    </Button>
                                </td>
                            </tr>
                        )}
                    </tbody>
                </table>  
                <GoogleLogout
                  clienId = {client_id}
                  buttonText="Logout"
                  onLogoutSuccess={onLogoutSuccess}
                />
        </Paper>
    </Container>
  );
}

