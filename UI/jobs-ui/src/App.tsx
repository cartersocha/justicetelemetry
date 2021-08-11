import React, { useState } from 'react';
import logo from './logo.svg';
import './App.css';
import { Button, Container } from 'semantic-ui-react';
import axios from 'axios';

import { ConsoleSpanExporter, SimpleSpanProcessor } from '@opentelemetry/sdk-trace-base';
import { WebTracerProvider } from '@opentelemetry/sdk-trace-web';

import { ZipkinExporter } from '@opentelemetry/exporter-zipkin';


const provider = new WebTracerProvider();

provider.addSpanProcessor(new SimpleSpanProcessor(new ZipkinExporter()));
provider.register();

const tracer = provider.getTracer('example-tracer-web');

// eslint-disable-next-line import/no-anonymous-default-export
export default function() {
  const parentSpan = tracer.startSpan('foo');
  doWork();
  let [jobs, setJobs] = useState();
  let getJobsList = (): any=>{
    axios.get("https://localhost:5001/api/Job").then(res => {
      const span = tracer.startSpan('API');
      console.log(res);
      setJobs(res.data);
      span.end(); 
    })
    parentSpan.end();
  }

function doWork() {
    // Start another span. In this example, the main method already started a
    // span, so that'll be the parent span, and this will be a child span.
    const span = tracer.startSpan('doWork');
  
    // simulate some random work.
    for (let i = 0; i <= Math.floor(Math.random() * 40000000); i += 1) {
      // empty
    }
    span.end();
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


