import React, { useState } from 'react';
import logo from './logo.svg';
import './App.css';
import { Button, Container } from 'semantic-ui-react';
import axios from 'axios';





export default function() {
  let [jobs, setJobs] = useState();

  let getJobsList = (): any=>{
    axios.get("https://localhost:44362/api/Job").then(res => {
      console.log(res);
      setJobs(res.data);
    })
  }

  return (
    <div className="App">
      <Button onClick={getJobsList}>Get Jobs</Button>

      {jobs && 
        <Container>{JSON.stringify(jobs)}</Container>
      }
    </div>
  );
}


