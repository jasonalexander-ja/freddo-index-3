import React from 'react';
import ReactDOM from 'react-dom';

import { ThemeProvider } from '@material-ui/core';
import {
    CssBaseline
} from '@material-ui/core';

import App from './App';

import useTheme from './theme';

const Main = () => {
    const theme = useTheme();

    return (
        <ThemeProvider theme={theme}>
            <CssBaseline />
            <App />
        </ThemeProvider>
    );
}

ReactDOM.render(
    <Main />,
    document.getElementById('root')
);
