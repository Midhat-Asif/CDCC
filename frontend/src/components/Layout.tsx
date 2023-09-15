import React from 'react';
import { Container } from 'reactstrap';
import NavMenu from './NavMenu';

interface LayoutProps {
  children: React.ReactNode;
}

const Layout: React.FC<LayoutProps> = ({ children }) => {
  return (
    <div>
      <NavMenu />
      <Container tag="main">{children}</Container>
    </div>
  );
};

export default Layout;
