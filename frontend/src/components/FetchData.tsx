import React, { useEffect, useState } from 'react';

interface Forecast {
  date: string;
  temperatureC: number;
  temperatureF: number;
  summary: string;
}

const FetchData: React.FC = () => {
  const [forecasts, setForecasts] = useState<Forecast[]>([]);
  const [loading, setLoading] = useState<boolean>(true);

  useEffect(() => {
    const populateWeatherData = async () => {
      try {
        const response = await fetch('http://localhost:5174/api/weatherforecast');
        const data = await response.json();
        setForecasts(data);
        setLoading(false);
      } catch (error) {
        console.error('Error fetching weather data:', error);
      }
    };

    populateWeatherData();
  }, []);

  const renderForecastsTable = (forecastData: Forecast[]) => {
    return (
      <table className="table table-striped" aria-labelledby="tableLabel">
        <thead>
          <tr>
            <th>Date</th>
            <th>Temp. (C)</th>
            <th>Temp. (F)</th>
            <th>Summary</th>
          </tr>
        </thead>
        <tbody>
          {forecastData.map((forecast) => (
            <tr key={forecast.date}>
              <td>{forecast.date}</td>
              <td>{forecast.temperatureC}</td>
              <td>{forecast.temperatureF}</td>
              <td>{forecast.summary}</td>
            </tr>
          ))}
        </tbody>
      </table>
    );
  };

  const contents = loading ? (
    <p>
      <em>Loading...</em>
    </p>
  ) : (
    renderForecastsTable(forecasts)
  );

  return (
    <div>
      <h1 id="tableLabel">Weather forecast</h1>
      <p>This component demonstrates fetching data from the server.</p>
      {contents}
    </div>
  );
};

export default FetchData;
