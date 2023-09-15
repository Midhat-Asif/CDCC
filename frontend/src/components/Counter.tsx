import React, { useState } from 'react';

interface CounterState {
  currentCount: number;
}

const Counter: React.FC<{}> = () => {
  const [state, setState] = useState<CounterState>({ currentCount: 0 });

  const incrementCounter = () => {
    setState((prevState) => ({
      currentCount: prevState.currentCount + 1,
    }));
  };

  return (
    <div>
      <h1>Counter</h1>

      <p>This is a simple example of a React component.</p>

      <p aria-live="polite">
        Current count: <strong>{state.currentCount}</strong>
      </p>

      <button className="btn btn-primary" onClick={incrementCounter}>
        Increment
      </button>
    </div>
  );
};

export default Counter;
