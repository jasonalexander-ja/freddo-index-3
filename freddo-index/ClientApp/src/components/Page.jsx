import React, { useState } from 'react';

import {
    Grid,
    Card,
    Typography,
    CardContent,
    FormControl,
    InputLabel,
    Select,
    MenuItem
} from '@material-ui/core';
import {
    ArgumentAxis,
    ValueAxis,
    Chart,
    LineSeries,
} from '@devexpress/dx-react-chart-material-ui';
import {
    makeStyles,
    useTheme
} from '@material-ui/core/styles';

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
        padding: 0,
        paddingBottom: '0px!important'
    },
    line: {
        padding: '16px'
    },
    lineDark: {
        padding: '16px',
        backgroundColor: theme.palette.background.paper
    }
}));

const Page = props => {
    const classes = useStyles();
    const theme = useTheme();

    const [age, setAge] = useState('10');

    const handleChange = (event) => {
        setAge(event.target.value);
    };

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
                md={5}
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
                            <Grid
                                item
                                container
                                xs={12}
                                justifyContent="space-around"
                                className={classes.lineDark}
                            >
                                <FormControl color="secondary">
                                    <InputLabel id="demo-simple-select-label">Age</InputLabel>
                                    <Select
                                        labelId="demo-simple-select-label"
                                        id="demo-simple-select"
                                        value={age}
                                        label="Age"
                                        onChange={handleChange}
                                    >
                                        <MenuItem value={10}>Ten</MenuItem>
                                        <MenuItem value={20}>Twenty</MenuItem>
                                        <MenuItem value={30}>Thirty</MenuItem>
                                    </Select>
                                </FormControl>
                                <FormControl color="secondary">
                                    <InputLabel id="demo-simple-select-label">Age</InputLabel>
                                    <Select
                                        labelId="demo-simple-select-label"
                                        id="demo-simple-select"
                                        value={age}
                                        label="Age"
                                        onChange={handleChange}
                                    >
                                        <MenuItem value={10}>Ten</MenuItem>
                                        <MenuItem value={20}>Twenty</MenuItem>
                                        <MenuItem value={30}>Thirty</MenuItem>
                                    </Select>
                                </FormControl>
                            </Grid>
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
// az sql server create --name FreddoIndexProd --resource-group FreddoIndex --location "UK West" --admin-user fred --admin-password HackspaceFreddo01!! 
export default Page;
