import React from 'react';

import {
    Grid,
    Card,
    Typography,
    CardContent
} from '@mui/material';
import {
    ArgumentAxis,
    ValueAxis,
    Chart,
    LineSeries,
} from '@devexpress/dx-react-chart-material-ui';
import {
    makeStyles,
    useTheme
} from '@mui/styles';

import OptionsPanel from './OptionsPanel';

const useStyles = makeStyles(theme => ({
    card: {
        display: 'flex',
        flexDirection: 'column',
        justifyContent: 'flex-start',
        backgroundColor: theme.palette.primary.main,
        background: 'rgba(128,128,128,1)'
    },
    cardContent: {
        flexGrow: 1,
        padding: '0px!important'
    },
    line: {
        padding: theme.spacing(2),
        backgroundColor: theme.palette.primary.main
    },
    lineDark: {
        padding: theme.spacing(2),
        backgroundColor: theme.palette.grey[800]
    }
}));

const Page = props => {
    const classes = useStyles();
    const theme = useTheme();

    const data = [
        { argument: 1, value: 1 },
        { argument: 2, value: 2 },
        { argument: 3, value: 3 },
    ];

    return (
        <>
            <Grid
                item
                xs={11}
                sm={7}
                md={6}
            >
                <Card className={classes.card}>
                    <CardContent className={classes.cardContent}>
                        <Grid
                            conatiner
                        >
                            <Grid
                                item
                                xs={12}
                                justifyContent="center"
                                className={classes.line}
                            >
                                <Typography style={{ textAlign: "center", fontWeight: 500 }} variant="h4">International Freddo Index</Typography>
                            </Grid>
                            <OptionsPanel />
                            <Grid
                                item
                                xs={12}
                                justifyContent="center"
                                className={classes.lineDark}
                            >
                                <Chart
                                    data={data}
                                    height={250}
                                >
                                    <ArgumentAxis />
                                    <ValueAxis />

                                    <LineSeries 
                                        valueField="value" 
                                        argumentField="argument"
                                        color={theme.palette.secondary.main}
                                    />
                                </Chart>
                            </Grid>
                            <Grid
                                item
                                xs={12}
                                justifyContent="center"
                                className={classes.line}
                            />
                        </Grid>
                    </CardContent>
                </Card>
            </Grid>
        </>
    );
}
 
export default Page;
