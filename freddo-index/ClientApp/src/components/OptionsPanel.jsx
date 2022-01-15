import react, { useState } from 'react';

import {
    Grid,
    FormControl,
    InputLabel,
    Select,
    MenuItem,
    TextField,
    useMediaQuery,
    Typography
} from '@mui/material';

import {
    DesktopDatePicker,
    MobileDatePicker
} from '@mui/lab';

import {
    makeStyles
} from '@mui/styles';

const useClasses = makeStyles(theme => ({
    line: {
        padding: '16px'
    },
    lineDark: {
        padding: '16px',
        backgroundColor: theme.palette.grey[800]
    },
    optionContainer: {
        width: '100%',
        margin: '8px'
    }
}));

export default function OptionsPanel(props) {
    const classes = useClasses();
    const showDesktop = useMediaQuery(theme => theme.breakpoints.up('sm'));

    const [date, setDate] = useState();

    return (
        <Grid
            item
            container
            xs={12}
            justifyContent="space-around"
            alignItems="center"
            className={classes.lineDark}
        >
            <Grid
                item
                container
                md={5}
                xs={12}
                justifyContent="flex-start"
            >
                <FormControl 
                    color="secondary" 
                    sx={{ m: 1, minWidth: 100 }}
                    className={classes.optionContainer}
                >
                    <InputLabel>Currency</InputLabel>
                    <Select
                        label="Currency"
                        color="secondary"
                        autoWidth
                    >
                        <MenuItem value={10}>Ten</MenuItem>
                        <MenuItem value={20}>Twenty</MenuItem>
                        <MenuItem value={30}>Thirty</MenuItem>
                    </Select>
                </FormControl>
            </Grid>
            <Grid
                item
                container
                md={7}
                xs={12}
                justifyContent="flex-start"
            >
                <FormControl 
                    color="secondary" 
                    sx={{ m: 1, minWidth: 100 }}
                    className={classes.optionContainer}
                >
                    {showDesktop ? 
                        <DesktopDatePicker
                            label="Date desktop"
                            inputFormat="MM/dd/yyyy"
                            color="secondary"
                            value={date}
                            onChange={setDate}
                            renderInput={(params) => <TextField color="secondary" {...params} />}
                        /> :
                        <MobileDatePicker
                            className={classes.optionContainer}
                            renderInput={params => 
                                <TextField 
                                    color="secondary" 
                                    {...params} 
                                />
                            }
                            label="Test"
                        />}
                    </FormControl>
            </Grid>
        </Grid>
    );
}
