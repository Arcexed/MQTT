import './Header.css' 
import userPhoto from './icons/User-photo.jpg'
import searchIcon from './icons/Search.svg'
import eventsIcon from './icons/Events.svg'
function Header(){
    return (
        <div className="header">
            <div className="header__inner">
                <span>Overview</span>
                <ul className="header__items-blocks">
                    <li className="header__items-block-item">
                        <ul className="header__items">
                            <li className="header__item">
                                <a href="" className="header__item-link">
                                    <img src={searchIcon} alt="" className="header__item-img"/>
                                </a>
                            </li>
                            <li className="header__item">
                                <a href="" className="header__item-link">
                                    <img src={eventsIcon} alt="" className="header__item-img"/>
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
                                    <img src={userPhoto} alt="" className="header__item-link-img"/>
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