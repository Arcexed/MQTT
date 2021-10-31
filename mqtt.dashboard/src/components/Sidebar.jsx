const Sidebar = () => {
  return (
      <div className="sidebar">
        <a href="" className="sidebar__logo">
          <img src="images/logo.svg" alt="" className="sidebar__logo-img"/>
            <p className="sidebar__logo-text">MQTT Dashboard</p>
        </a>
        <nav className="sidebar__menu">
          <ul className="sidebar__menu-list">
            <li className="sidebar__menu-item">
              <a href="" className="sidebar__menu-list-link">
                <img src="images/icons/Overview.svg" alt="" className="sidebar__menu-list-img"/>
                  Dashboard
              </a>
            </li>
            <li className="sidebar__menu-item">
              <a href="" className="sidebar__menu-list-link">
                <img src="images/icons/Overview.svg" alt="" className="sidebar__menu-list-img"/>
                  Devices
              </a>
            </li>
            <li className="sidebar__menu-item">
              <a href="" className="sidebar__menu-list-link">
                <img src="images/icons/Overview.svg" alt="" className="sidebar__menu-list-img"/>
                  Reports
              </a>
            </li>
            <li className="sidebar__menu-item">
              <a href="" className="sidebar__menu-list-link">
                <img src="images/icons/Overview.svg" alt="" className="sidebar__menu-list-img"/>
                  API
              </a>
            </li>
            <li className="sidebar__menu-item">
              <a href="" className="sidebar__menu-list-link">
                <img src="images/icons/Overview.svg" alt="" className="sidebar__menu-list-img"/>
                  TEST
              </a>
            </li>
          </ul>
        </nav>
      </div>
  );
}
export default Sidebar;