import React from 'react';
import Sidebar from "./components/Sidebar/Sidebar";
import './Reset.css';
import './App.css';
import Header from "./components/Header/Header";

function App() {
    return (
        <div className="App">
            <div className="body">
                <Sidebar/>
                <div className="rightSide">
                    <Header/>
                    <div className="content">
                        <div className="content__box-list">
                            <div className="content__box-item">
                                <p>Unresolved</p>
                                60
                            </div>
                            <div className="content__box-item">
                                <p>Overdue</p>
                                16
                            </div>
                            <div className="content__box-item">
                                <p>Open</p>
                                43
                            </div>
                            <div className="content__box-item">
                                <p>On Hold</p>
                                64
                            </div>
                        </div>
                        <div className="content__chart-box">
                            <div className="content__chart-item">

                            </div>
                            <div className="content__chart-list">
                                <div className="content__chart-item">
                                    <p>Resolved</p>
                                    449
                                </div>
                                <div className="content__chart-item">
                                    <p>Received</p>
                                    426
                                </div>
                                <div className="content__chart-item">
                                    <p>Average first response time</p>
                                    33m
                                </div>
                                <div className="content__chart-item">
                                    <p>Average response time</p>
                                    3h 8m
                                </div>
                                <div className="content__chart-item">
                                    <p>Resolution within SLA</p>
                                    94%
                                </div>
                            </div>
                        </div>
                    </div>
                </div>

            </div>
        </div>
    );
}

export default App;
