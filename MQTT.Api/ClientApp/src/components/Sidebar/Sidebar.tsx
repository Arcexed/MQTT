import './Sidebar.css';
import logo from './icons/logo.svg';
import icon_overview from './icons/Overview.svg'

function Sidebar() {
    return (
        <div className="sidebar">
            <a href="" className="sidebar__logo">
                <img src={logo} alt="" className="sidebar__logo-img"/>
                MQTT Dashboard
            </a>
            <nav className="sidebar__menu">
                <a href="" className="sidebar__menu-list-link">
                    <img src={icon_overview} alt="" className="sidebar__menu-list-img"/>
                    Dashboard
                </a>
                <a href="" className="sidebar__menu-list-link">
                    <img src={icon_overview} alt="" className="sidebar__menu-list-img"/>
                    Devices
                </a>
                <a href="" className="sidebar__menu-list-link">
                    <img src={icon_overview} alt="" className="sidebar__menu-list-img"/>
                    Reports
                </a>
                <a href="" className="sidebar__menu-list-link">
                    <img src={icon_overview} alt="" className="sidebar__menu-list-img"/>
                    API
                </a>
                <a href="" className="sidebar__menu-list-link">
                    <img src={icon_overview} alt="" className="sidebar__menu-list-img"/>
                    TEST
                </a>
            </nav>
        </div>
    );
}

export default Sidebar;