const Header = () => {
    return (
        <div className="header">
            <div className="header__inner">
                <p className="header__title-text">Overview</p>
                <ul className="header__items-blocks">
                    <li className="header__items-block-item">
                        <ul className="header__items">
                            <li className="header__item">
                                <a href="" className="header__item-link">
                                    <img src="images/icons/Search.svg" alt="" className="header__item-img"/>
                                </a>
                            </li>
                            <li className="header__item">
                                <a href="" className="header__item-link">
                                    <img src="images/icons/Events.svg" alt="" className="header__item-img"/>
                                </a>
                            </li>
                        </ul>
                    </li>
                    <li className="header__items-block-item">
                        <ul className="header__items">
                            <li className="header__item">
                                <p className="header__item-text">
                                    Jones Ferdinand
                                </p>
                            </li>
                            <li className="header__item">
                                <a href="" className="header__item-link">
                                    <img src="images/User-photo.jpg" alt="" className="header__item-link-img"/>
                                </a>
                            </li>
                        </ul>
                    </li>
                </ul>
            </div>
        </div>
    );
}
export default Header;