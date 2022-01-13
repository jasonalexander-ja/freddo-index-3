import React from 'react';
import ReactDOM from 'react-dom';

import { 
    ThemeProvider 
} from '@mui/material/styles';
import {
    ThemeProvider as LegacyThemeProvider
} from '@material-ui/core'
import {
    CssBaseline
} from '@mui/material';

import {
    LocalizationProvider
} from '@mui/lab';
import DateAdapter from '@mui/lab/AdapterDateFns';


import App from './App';

import useTheme, { useLegacyTheme } from './theme';

const Main = () => {
    const theme = useTheme();
    const legacyTheme = useLegacyTheme();

    return (
        <LocalizationProvider dateAdapter={DateAdapter}>
            <ThemeProvider theme={theme}>
                <LegacyThemeProvider theme={legacyTheme}>
                    <CssBaseline />
                    <App />
                </LegacyThemeProvider>
            </ThemeProvider>
        </LocalizationProvider>
    );
}

ReactDOM.render(
    <Main />,
    document.getElementById('root')
);
