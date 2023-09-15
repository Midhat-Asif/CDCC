import React, { useState, useEffect } from 'react';
import axios from 'axios';
import '../styles/table.css';

interface SalesData {
  salesID: number;
  orderDate: string;
  region: string;
  rep: string;
  item: string;
  units: number;
  unitCost: number;
  total: number;
}

const FilteredSalesTable: React.FC = () => {
  const [projectIDs, setProjectIDs] = useState<string[]>([]);
  const [selectedProjectID, setSelectedProjectID] = useState<string>('');
  const [filteredData, setFilteredData] = useState<SalesData[]>([]);

  // Fetch unique ProjectIDs
  useEffect(() => {
    axios.get<string[]>('http://localhost:5174/api/projectids')
      .then((response) => {
        setProjectIDs(response.data); // Assuming the API endpoint returns an array of unique ProjectIDs
      })
      .catch((error) => {
        console.error('Error fetching ProjectIDs:', error);
      });
  }, []);

  // Handle ProjectID selection
  const handleProjectIDChange = (event: React.ChangeEvent<HTMLSelectElement>) => {
    setSelectedProjectID(event.target.value);
  };

  // Fetch filtered data when ProjectID changes
  useEffect(() => {
    if (selectedProjectID) {
      axios.get<SalesData[]>(`http://localhost:5174/api/sales?projectID=${selectedProjectID}`)
        .then((response) => {
          setFilteredData(response.data); // Assuming the API endpoint returns filtered data
        })
        .catch((error) => {
          console.error('Error fetching filtered data:', error);
        });
    }
  }, [selectedProjectID]);

  return (
    <div>
      <h2>Filter by ProjectID</h2>
      <select value={selectedProjectID} onChange={handleProjectIDChange}>
        <option value="">Select a ProjectID</option>
        {projectIDs.map((projectID) => (
          <option key={projectID} value={projectID}>
            {projectID}
          </option>
        ))}
      </select>
      <br /><br />
      {selectedProjectID && (
        <table>
          <thead>
            <tr>
              <th>OrderDate</th>
              <th>Region</th>
              <th>Rep</th>
              <th>Item</th>
              <th>Units</th>
              <th>UnitCost</th>
              <th>Total</th>
            </tr>
          </thead>
          <tbody>
            {filteredData.map((data) => (
              <tr key={data.salesID}>
                <td>{data.orderDate}</td>
                <td>{data.region}</td>
                <td>{data.rep}</td>
                <td>{data.item}</td>
                <td>{data.units}</td>
                <td>{data.unitCost}</td>
                <td>{data.total}</td>
              </tr>
            ))}
          </tbody>
        </table>
      )}
    </div>
  );
};

export default FilteredSalesTable;
