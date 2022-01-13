import React from 'react';

import { 
    createStyles, 
    makeStyles 
} from '@mui/styles';
import { 
    Paper,
    Grid
} from '@mui/material';

import Page from './components/Page'

import backgroundImage from './images/freddo.jpg';

const useStyles = makeStyles(theme =>
    createStyles({
        root: {
            width: '100%', 
            height: '100%',
            position: 'relative',
            opacity: 1,
            display: 'flex',
            alignItems: 'center',
            justifyContent: 'center',
            '&::after': {
                content: '""',
                backgroundImage: `url("${backgroundImage}")`,
                zIndex: 100,
                backgroundSize: 'cover',
                position: 'absolute',
                top: 0,
                right: 0,
                bottom: 0,
                left: 0,
                opacity: 0.5,
            }
        },
        content: {
            height: '100%',
            zIndex: 1000,
            paddingTop: theme.spacing(1)
        }
    }),
);

const App = props => {
    const classes = useStyles();

    return (
        <Paper className={classes.root}>
            <Grid
                container
                xs={12}
                className={classes.content}
            >
                <Grid
                    item
                    xs={null}
                    lg={2}
                />
                <Grid
                    item
                    container
                    justifyContent="center"
                    xs={12}
                    lg={8}
                >
                    <Grid
                        container
                        justifyContent="center"
                        spacing={12}
                    >
                        <Page />
                    </Grid>
                </Grid>
                <Grid
                    item
                    xs={null}
                    lg={2}
                />
            </Grid>
        </Paper>
    );
}

export default App;
