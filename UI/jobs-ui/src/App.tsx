import React, { useState } from 'react';
import logo from './logo.svg';
import './App.css';
import { Button, Container } from 'semantic-ui-react';
import axios from 'axios';

import { ConsoleSpanExporter, SimpleSpanProcessor } from '@opentelemetry/sdk-trace-base';
import { WebTracerProvider } from '@opentelemetry/sdk-trace-web';
import { registerInstrumentations } from '@opentelemetry/instrumentation';
import { ZipkinExporter } from '@opentelemetry/exporter-zipkin';
import { B3Propagator } from '@opentelemetry/propagator-b3';
import { XMLHttpRequestInstrumentation } from '@opentelemetry/instrumentation-xml-http-request';
import { ZoneContextManager } from '@opentelemetry/context-zone';
import { SemanticResourceAttributes } from '@opentelemetry/semantic-conventions';

const { Resource } = require('@opentelemetry/resources');
const api = require('@opentelemetry/api');


const provider = new WebTracerProvider({
  resource: new Resource({
    [SemanticResourceAttributes.SERVICE_NAME]: 'Carter-Frontend',
  }),
});

provider.addSpanProcessor(new SimpleSpanProcessor(new ZipkinExporter()));
provider.register();


provider.register({
  contextManager: new ZoneContextManager(),
  propagator: new B3Propagator(),

});

registerInstrumentations({
  instrumentations: [
    new XMLHttpRequestInstrumentation({
      propagateTraceHeaderCorsUrls: ['http://localhost:5001']
    }),
  ],
});

const tracer = provider.getTracer('example-tracer-web');

// eslint-disable-next-line import/no-anonymous-default-export
export default function() {
  const parentSpan = tracer.startSpan('foo');
  doWork(parentSpan);
  let [jobs, setJobs] = useState();
  let getJobsList = (): any=>{
    axios.get("https://localhost:5001/api/Job").then(res => {
      const ctx = api.trace.setSpan(api.context.active(), parentSpan);
      const span = tracer.startSpan('API', undefined, ctx);
      console.log(res);
      setJobs(res.data);
      span.end(); 
      
    })

  }
  parentSpan.end();

function doWork(parentSpan: any) {
    // Start another span. In this example, the main method already started a
    // span, so that'll be the parent span, and this will be a child span.
    const ctx = api.trace.setSpan(api.context.active(), parentSpan);
    const span = tracer.startSpan('doWork', undefined, ctx);
  
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


